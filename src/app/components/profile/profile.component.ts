import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: any;
  constructor(private authService: AuthService) { }

  ngOnInit() {
    console.log('in profile');
    this.authService.getProfile()
      .subscribe(data => this.user = data.user);
  }

}
