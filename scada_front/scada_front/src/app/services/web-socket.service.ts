import { Injectable } from '@angular/core';

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
