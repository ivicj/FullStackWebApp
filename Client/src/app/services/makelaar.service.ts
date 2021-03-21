import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Makelaar } from '../models/makelaar.model';

@Injectable({
  providedIn: 'root',
})
export class MakelaarService {
  constructor(private http: HttpClient) {}

  getMakelaars(tuin: boolean): Observable<Makelaar[]> {
    var a = this.http.get<Makelaar[]>('https://localhost:44381/api/Aanbod/LeaderBoard/' + tuin);
    console.log('aaaa',a);
    return a;
  }
}
