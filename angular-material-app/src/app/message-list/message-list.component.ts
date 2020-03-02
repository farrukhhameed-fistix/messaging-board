import { Component, OnInit, Input } from '@angular/core';
import {MessageModel} from '../models/messageModel';

@Component({
  selector: 'app-message-list',
  templateUrl: './message-list.component.html',
  styleUrls: ['./message-list.component.css']
})
export class MessageListComponent {

  @Input() messages: MessageModel[];

  constructor() {
  }

}
