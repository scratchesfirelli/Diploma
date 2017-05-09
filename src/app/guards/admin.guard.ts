import { AuthService } from 'app/services/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from "@angular/router";

@Injectable()
export class AdminGuard implements CanActivate {

    constructor(private authService: AuthService, private router: Router) { }

    canActivate() {
        if(this.authService.isAdmin()) {
            return true;
        } else {
            this.router.navigate(['/login']);
            return false;
        }
    }
}