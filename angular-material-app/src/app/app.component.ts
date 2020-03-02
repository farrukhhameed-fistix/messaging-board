import { Component, OnInit } from '@angular/core';
import { IdentityService } from './services/identity.service';
import { SignalRService } from './services/signal-r.service';
import * as moment from 'moment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(public signalRService: SignalRService, private identityService:IdentityService){
    
  }

  ngOnInit() {
    this.identityService.setCurrentUser('john smith - ' + moment(new Date()).format('x'));
    this.signalRService.startConnection();
    this.signalRService.addReceiveMessageListener();      
  }
}
