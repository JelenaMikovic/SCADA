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

  getTags(): Observable<TagDTO[]> {
    return this.http.get<any>(`${this.baseUrl}`);
  }

  deleteTag(id: number) : Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/` + id);
  }

  toggleTag(id: number) : Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/` + id, id);
  }

  editTag(dto: TagDTO): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}`, dto);
  }

  createTag(dto: { name: string; ioAddress: string; description: string; value: number; lowLimit?: number; highLimit?: number; scanTime?: number; isScanOn?: boolean; type: string; unit?: string; }): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}`, dto);
  }

}



export interface TagDTO {

  id: number;
  name:string;
  description: string;
  ioAddress:string;
  value: number;
  lowLimit?: number;
  highLimit?: number;
  unit?: string;
  tagType: string;
  isScanOn?: boolean;
  scanTime?: number;
}