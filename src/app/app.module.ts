import { ProductService } from './services/product.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ProductComponent } from './components/product/product.component';
import { Routes, RouterModule } from "@angular/router";
import { LoginComponent } from './components/login/login.component';
import { ProductFormComponent } from './components/product-form/product-form.component';
import { ProductListComponent } from './components/product-list/product-list.component';

const routes: Routes = [
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'login', component: LoginComponent},
            { path: 'product',
                children: [
                    {path: '', redirectTo: 'list/10', pathMatch: 'full'},
                    {path: 'list/:num', component: ProductListComponent},
                    {path: 'create', component: ProductFormComponent},
                    {path: ':id', component: ProductComponent}
                ]
            },
            { path: '**', redirectTo: 'home' }
];

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    ProductComponent,
    LoginComponent,
    ProductFormComponent,
    ProductListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    RouterModule.forRoot(routes)
  ],
  providers: [ProductService],
  bootstrap: [AppComponent]
})
export class AppModule { }
