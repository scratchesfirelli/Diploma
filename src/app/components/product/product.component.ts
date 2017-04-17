import { Observable } from 'rxjs/Observable';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product } from './../../models/product';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  product: Product;
  constructor(private productService: ProductService, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
      const id = this.activatedRoute.snapshot.params['id'];
      this.productService.getById(id)
          .subscribe(product => this.product = product);
      console.log(this.product);
  }

}
