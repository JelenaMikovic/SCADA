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

  getTags(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}`);
  }


}



export interface TagDTO {

  id: number;
  name:string;
  description: string;
  iOAddress:string;
  value: number;
  lowLimit?: number;
  highLimit?: number;
  unit?: string;
  tagType: string;
  isScanOn?: boolean;
  scanTime?: number;
}
