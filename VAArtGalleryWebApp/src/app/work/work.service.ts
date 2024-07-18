import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Work } from './models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WorkService {
  private baseUrl = 'https://localhost:7042/api/art-works'
  constructor(private http: HttpClient) { }

  getWorks(id: string): Observable<Work[]> {
    return this.http.get<Work[]>(`${this.baseUrl}`, {params: {artGalleryId: id}});
  }
}
