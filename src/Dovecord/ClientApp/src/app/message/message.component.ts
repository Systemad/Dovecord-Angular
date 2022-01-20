import { Component, OnInit, EventEmitter, Output, Input, ChangeDetectionStrategy, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TUI_VALIDATION_ERRORS } from '@taiga-ui/kit';
import { ChannelMessageDto } from '../web-api-client';
import { TuiHostedDropdownComponent } from '@taiga-ui/core';

export function maxLengthMessageFactory(context: {requiredLength: string}): string {
  return `Maximum length — ${context.requiredLength}`;
}

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})

export class MessageComponent implements OnInit {

  @ViewChild(TuiHostedDropdownComponent)
  component?: TuiHostedDropdownComponent;

  @Input() message?: ChannelMessageDto;
  @Output() deleteMessage: EventEmitter<string> = new EventEmitter();
  @Output() editMessage: EventEmitter<ChannelMessageDto> = new EventEmitter();

  constructor() {}

  ngOnInit(): void {
  }

  readonly items = ['edit', 'delete', 'info'];

  open = false;

  onClick(item: string) {

    switch(item){
      case "delete":{
        this.deleteMessage.emit(this.message?.id);
        break;
      }
      case "edit":{
        this.editMessage.emit(this.message)
        //this.deleteMessage.emit(this.message);
        break;
      }
      case "info":{
        console.log("info clicked");
        //this.deleteMessage.emit(this.message);
        break;
      }
    }

      this.open = false;

      if (this.component && this.component.nativeFocusableElement) {
          this.component.nativeFocusableElement.focus();
      }
  }
}