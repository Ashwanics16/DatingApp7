import { Component, OnInit } from '@angular/core';
import { TitleStrategy } from '@angular/router';
import { User } from './_models/user';
import { AccountService } from './_Services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'Datting App'; 
  users:any;
  constructor(private accountService : AccountService) {}

  ngOnInit(): void {
    this.setCurrentUser();
    
  }

   setCurrentUser(){
    const userString= localStorage.getItem('user');
    if(!userString) return;
    const user : User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
   }


  } 
 

