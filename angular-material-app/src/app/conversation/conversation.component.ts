import { Component, OnInit } from '@angular/core';
import {MessagingService} from '../services/messaging.service';
import {IdentityService} from '../services/identity.service';

@Component({
  selector: 'app-conversation',
  templateUrl: './conversation.component.html',
  styleUrls: ['./conversation.component.css']
})
export class ConversationComponent implements OnInit {

  constructor(private identityService: IdentityService, public messagingService: MessagingService) {     
  }
  
  onNewMessageEntered(message: string){
    console.log(message);
    this.messagingService.saveNewMessage(message, this.identityService.getCurrentUser());
  }

  ngOnInit(): void {    
    this.messagingService.getAllMessages().subscribe();
  }

}
