import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import { MatTableDataSource } from '@angular/material/table';


@Injectable({
  providedIn: 'root'
})
export class AlarmService {
 constructor(private http: HttpClient) { }
  private baseUrl = 'http://localhost:5184/api/Alarm';

  deleteAlarm(id: number) : Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/` + id);
  }

  getTagsAlarms(tagId: number) {
    return this.http.get<any>(`${this.baseUrl}/` + tagId);
  }

  addAlarm(dto: { value: number; priority: number; type: number; tagId: number; }) {
    return this.http.post<any>(`${this.baseUrl}/`, dto);
  }

  getAlarmRecords():Observable<AlarmRecordDTO[]>{
    return this.http.get<any>(`${this.baseUrl}/`);
  }


  getAlarmTimeReport(date1:Date,date2:Date):Observable<AlarmRecordDTO[]>{
    return this.http.post<any>(`${this.baseUrl}/dates`,{startTime:date1,endTime:date2});
  }

  getAlarmPriorityReport(priority:string):Observable<AlarmRecordDTO[]>{
    return this.http.get<any>(`${this.baseUrl}/priority/` + priority);
  }

}

export interface AlarmDTO {
  id: number;
  tagId: string;
  type: string;
  priority: string;
  value: number;
}

export interface AlarmRecordDTO{
  id:number
  alarmId:number;
  tagId:number;
  timestamp:Date;
}
