import { Router } from '@angular/router';
import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  userName: String;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.authService.getProfile()
  }

  onLogoutClick() {
    this.authService.logOut();
    this.router.navigate(['login']);
  }
}
