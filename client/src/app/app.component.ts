import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating App';
  users: any;

  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser() {
    //As the error says, localStorage.getItem() can return either a string or null.JSON.parse() requires a string, so you should test the result of localStorage.getItem() before you try to use it.
    //For example:
    //const user: User = JSON.parse(localStorage.getItem('user') || '{}');
    //or perhaps:
    //const userJson = localStorage.getItem('user');
    //const user = userJson !== null ? JSON.parse(userJson) : new User();
    //See also the answer from Willem De Nys. If you are confident that the localStorage.getItem() call can never return null you can use the non-null assertion operator to tell typescript that you know what you are doing:
    const user: User = JSON.parse(localStorage.getItem('user')!);
    this.accountService.setCurrentUser(user);
  }
}
