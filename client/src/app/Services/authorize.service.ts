import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, catchError, map, Observable, of, throwError } from 'rxjs';
import { shareReplay, switchMap, take, tap, timeout } from 'rxjs/operators';

export type AuthLevels = "Guest"|"User"|"Admin";

/**
 *  Service for managing user authentication and authorization
 */
@Injectable({ providedIn: 'root' })
export class AuthService {

    /**
     * Constructor of AuthService.
     * @param {HttpClient} http  - The HttpClient instance to make HTTP requests.
     * @memberof AuthService
     */
    constructor(private http: HttpClient) {}

    /**
     *Sets the token in local storage.
     *
     * @private
     * @param {string} token - The token to be set.
     * @memberof AuthService
     */
    private setToken = (token: string) => {
        localStorage.setItem('token', token);
    }

    /**
     *  Gets the token from local storage.
     *
     * @memberof AuthService
     * 
     * @returns The token from local storage.
     */
    public getToken = () => localStorage.getItem('token');
}