import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.css']
})
export class ErrorComponent implements OnInit {
  baseurl = 'https://localhost:5001/api/';
  validationErrors: string[] = [];

  constructor(private httpclient: HttpClient) { }

  ngOnInit(): void {
  }

  get500Error() {
    this.httpclient.get(this.baseurl + 'error/server-error').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    })
  }

  get400Error() {
    this.httpclient.get(this.baseurl + 'error/bad-request').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    })
  }

  get401Error() {
    this.httpclient.get(this.baseurl + 'error/authorize').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    })
  }

  get404Error() {
    this.httpclient.get(this.baseurl + 'error/not-found').subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
    })
  }

  get400ValidationError() {
    this.httpclient.post(this.baseurl + 'account/register', {}).subscribe(response => {
      console.log(response);
    }, error => {
      console.log(error);
      this.validationErrors=error;
    })
  }
}
