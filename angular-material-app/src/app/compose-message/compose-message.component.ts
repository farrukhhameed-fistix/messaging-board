import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-compose-message',
  templateUrl: './compose-message.component.html',
  styleUrls: ['./compose-message.component.css']
})
export class ComposeMessageComponent {
  @Output() onMessageEntered = new EventEmitter<string>();  
  messageContent: string

  constructor() { }

  onEnter() {     
    this.onMessageEntered.emit(this.messageContent);
    this.messageContent = "";
  }

}
