import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import { MatTableDataSource } from '@angular/material/table';


@Injectable({
  providedIn: 'root'
})
export class TagService {

  constructor(private http: HttpClient) { }
  private baseUrl = 'http://localhost:5184/api/Tag';


  getAllOutputTagsDBManager(): Observable<TableOutputTag[]> {
    return this.http.get<any>(this.baseUrl + "/dbm/output", {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    });
  }

  getAllInputTags(): Observable<TableInputTag[]> {
    return this.http.get<any>(this.baseUrl + "/dbm/input", {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      })
    });
  }
}

export interface TableOutputTag {
  id: number,
  description: string,
  value: string,
  unit: string,
  type: any
}

export interface TableInputTag {
  alarmType?: any;
  alarmValue?: any;
  lowLimit?: any;
  highLimit?: any;
  id: number,
  description: string,
  unit: string,
  type: any,
  isScanOn: boolean,
  scanTime: number,
  value: number
}
