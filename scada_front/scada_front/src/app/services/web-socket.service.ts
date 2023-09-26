import { Injectable } from '@angular/core';
import {Subject} from "rxjs";
import * as SockJS from "sockjs-client";
import * as Stomp from 'stompjs';
import { HttpTransportType, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';


@Injectable({
  providedIn: 'root'
})
export class WebSocketService {
  constructor() { }


}


export interface TagRecordDTO {
  id:number;
  tagId: number;
  timestamp:Date;
  value: number;
  lowLimit?: number;
  highLimit?: number;
}
