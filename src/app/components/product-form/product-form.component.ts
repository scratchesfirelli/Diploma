import { Router } from '@angular/router';
import { ProductMaterial } from './../../models/productMaterial';
import { ProductType } from './../../models/productType';
import { Observable } from 'rxjs/Observable';
import { ProductService } from './../../services/product.service';
import { Product } from './../../models/product';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {

  productMaterials: Observable<ProductMaterial>;
  productTypes: Observable<ProductType>;
  form: FormGroup;

  constructor(private fb: FormBuilder, private productService: ProductService, private router: Router) { }


  ngOnInit() {
    this.form = this.fb.group({
      id: '',
      title: ['', Validators.required],
      description: '',
      price: ['', Validators.required],
      type: ['', Validators.required],
      material: '',
      weight: '',
      width: '',
      height: '',
      length: ''
    });
    this.productMaterials = this.productService.getProductMaterials();
    this.productTypes = this.productService.getProductTypes();
  }

  submitForm(form: any) {
    const product: Product = {
      Title: this.form.value.title,
      Description: this.form.value.description,
      Price: this.form.value.price,
      Rating: 0,
      ProductTypeId: this.form.value.type,
      Weight: this.form.value.weight,
      Width: this.form.value.width,
      Height: this.form.value.height,
      Length: this.form.value.length,
      ProductMaterialId: this.form.value.material,
      CreateDate: new Date()
    };
    this.productService.addProduct(product).subscribe(data => {
      if(data.success) {
        this.router.navigate(['/product']);
      }
    });
  }
}
