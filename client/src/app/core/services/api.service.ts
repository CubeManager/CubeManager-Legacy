import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, retry, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(private http: HttpClient) { }

  public get<T>(url: string, headers?: HttpHeaders): Observable<T> {
    return this.http.get<T>(url, { headers }).pipe(retry(3), catchError((error) => this.handleApiError(error)));
  }

  public post<T>(url: string, data: T, headers?: HttpHeaders): Observable<T> {
    return this.http.post<T>(url, data, { headers }).pipe(retry(3), catchError((error) => this.handleApiError(error)));
  }

  public put<T>(url: string, data: T, headers?: HttpHeaders): Observable<T> {
    return this.http.post<T>(url, data, { headers }).pipe(retry(3), catchError((error) => this.handleApiError(error)));
  }

  public delete<T>(url: string, data: T, headers?: HttpHeaders): Observable<T> {
    return this.http.delete<T>(url, { headers }).pipe(retry(3), catchError((error) => this.handleApiError(error)));
  }

  handleApiError(err: any): Observable<any> {
    let message = '';
    if (err.error instanceof ErrorEvent) {
      message = err.error.message;
    } else {
      message = `Error Code: ${err.status}\nMessage: ${err.message}`;
    }
    console.log(message);
    return throwError(() => {
      message;
    });
  }

}
