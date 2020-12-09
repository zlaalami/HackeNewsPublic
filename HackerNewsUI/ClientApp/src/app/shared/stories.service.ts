import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { appConfig } from '../config/app.config';

export interface HackerNewsStories {
  title: any;
  url: string;
  time: any;
  id: string;
  row: number;
}

@Injectable({
  providedIn: 'root'
})
export class StoriesService {

  REST_API: string = appConfig.hackernewsAPI;

  constructor(private http: HttpClient) { }

  getStories(): Observable<HackerNewsStories[]> {
    return this.http.get<HackerNewsStories[]>(`${this.REST_API}`);
  }
}
