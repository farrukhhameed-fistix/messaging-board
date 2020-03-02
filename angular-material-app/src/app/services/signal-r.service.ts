import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { MessagingService } from './messaging.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  constructor(private messagingService:MessagingService) { }

  private hubConnection: signalR.HubConnection
 
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('http://localhost:5000/messagingHub')
                            .build();
 
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }
 
  public addReceiveMessageListener = () => {
    this.hubConnection.on('ReceiveMessage', (user, message, messageTime) => {
      this.messagingService.appendMessage(message, user, new Date(messageTime));
    });
  }
}
