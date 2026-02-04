import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApproveOrRejectRequest, Request } from '../models/request.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = "http://localhost:5094/api";
  constructor(private http: HttpClient) { }

  async getDatas(): Promise<Request[]> {
    try {
      const response = await fetch(`${this.apiUrl}/Request`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json'
        }
      })

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      return await response.json();

    } catch (err) {
      console.error('Error fetching data:', err);
      throw err;
    }
  }

  async Approve(id: number, reason: ApproveOrRejectRequest): Promise<any> {
    try {
      const response = await fetch(`${this.apiUrl}/Request/${id}/approve`, {
        method: 'PATCH',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(reason)
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      console.log("pass");

      return await response.json();
    } catch (err) {
      console.error('Error patching data:', err);
      throw err;
    }
  }

  async Reject(id: number, reason: ApproveOrRejectRequest): Promise<any> {
    try {
      const response = await fetch(`${this.apiUrl}/Request/${id}/reject`, {
        method: 'PATCH',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(reason)
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      console.log("pass");

      return await response.json();
    } catch (err) {
      console.error('Error patching data:', err);
      throw err;
    }
  }
}
