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

 
}

export interface AlarmDTO {
  id: number;
  tagId: string;
  type: string;
  priority: string;
  value: number;
}
