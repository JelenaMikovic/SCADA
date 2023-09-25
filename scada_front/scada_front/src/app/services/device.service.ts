import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import { MatTableDataSource } from '@angular/material/table';


@Injectable({
  providedIn: 'root'
})
export class DeviceService {
  constructor(private http: HttpClient) { }
  private baseUrl = 'http://localhost:5184/api/Device';

  getDevices(): Observable<DeviceDTO[]> {
    return this.http.get<any>(`${this.baseUrl}`);
  }

}

export interface DeviceDTO {
  ioAddress:string;
  value: number;
  type: string;
}