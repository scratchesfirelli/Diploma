import { User } from './../../models/user';
import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  user: User;
  constructor(public authService: AuthService) { }

  ngOnInit() {
    this.authService.getProfile()
      .subscribe(data => this.user = data);
  }

}
