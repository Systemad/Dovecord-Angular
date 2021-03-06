import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { ChannelClient, MessageClient, UserClient, WeatherForecastClient } from './web-api-client';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './Components/nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ChannelComponent } from './Components/channel/channel.component';
import { ProfileComponent } from './profile/profile.component';
import { ChatComponent } from './chat/chat.component';
import { ChattestComponent } from './chattest/chattest.component';
import { MessageComponent } from './Components/message/message.component';
import { ChatInputComponent } from './Components/chat-input/chat-input.component';
import { ChatWindowComponent } from './Components/chat-window/chat-window.component';
import { HeaderBoxComponent } from './Components/header-box/header-box.component';
import { UserListComponent } from './Components/user-list/user-list.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IPublicClientApplication, PublicClientApplication, InteractionType, BrowserCacheLocation, LogLevel } from '@azure/msal-browser';
import { MsalGuard, MsalInterceptor, MsalBroadcastService, MsalInterceptorConfiguration, MsalModule, MsalService, MSAL_GUARD_CONFIG, MSAL_INSTANCE, MSAL_INTERCEPTOR_CONFIG, MsalGuardConfiguration, MsalRedirectComponent } from '@azure/msal-angular';
import { AppRoutingModule } from './app-routing.module'; // InteractionType added to imports

import { msalConfig, loginRequest, protectedResources } from './auth/auth-config';
import { MaterialModule } from './shared/material.module';
import { TaigaModule } from './shared/taiga.module';
import { ProfilecardComponent } from './Components/profilecard/profilecard.component';

export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication(msalConfig);
}

export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
  const protectedResourceMap = new Map<string, Array<string>>();

  protectedResourceMap.set(protectedResources.weatherApi.endpoint, protectedResources.weatherApi.scopes);
  protectedResourceMap.set(protectedResources.channelApi.endpoint, protectedResources.channelApi.scopes);
  protectedResourceMap.set(protectedResources.messageApi.endpoint, protectedResources.messageApi.scopes);
  protectedResourceMap.set(protectedResources.userApi.endpoint, protectedResources.userApi.scopes);
  protectedResourceMap.set(protectedResources.signalrhub.endpoint, protectedResources.signalrhub.scopes);

  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap
  };
}

export function MSALGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: loginRequest
  };
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ChannelComponent,
    ProfileComponent,
    ChatComponent,
    ProfilecardComponent,
    ChattestComponent,
    MessageComponent,
    ChatInputComponent,
    ChatWindowComponent,
    HeaderBoxComponent,
    UserListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    MaterialModule,
    TaigaModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MsalModule,
],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    },
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory
    },
    {
      provide: MSAL_GUARD_CONFIG,
      useFactory: MSALGuardConfigFactory
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MSALInterceptorConfigFactory
    },
    /*
    SignalRService, {
      provide: APP_INITIALIZER,
      useFactory: (signalRService: SignalRService) => () => signalRService.initiateSignalrConnection(),
      deps: [SignalRService],
      multi: true,
    },
    */
    MsalService,
    MsalGuard,
    MsalBroadcastService,
    WeatherForecastClient,
    ChannelClient,
    MessageClient,
    UserClient,
  ],
  bootstrap: [AppComponent, MsalRedirectComponent]
})
export class AppModule { }
