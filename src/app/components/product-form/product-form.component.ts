import { Router, ActivatedRoute } from '@angular/router';
import { ProductMaterial } from './../../models/productMaterial';
import { ProductType } from './../../models/productType';
import { Observable } from 'rxjs/Observable';
import { ProductService } from './../../services/product.service';
import { Product } from './../../models/product';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

import 'rxjs';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {

  title: String = 'Create new product';
  productMaterials: Observable<ProductMaterial>;
  productTypes: Observable<ProductType>;
  form: FormGroup;
  product: any = {};

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.form = this.fb.group({
      id: '',
      title: ['', Validators.required],
      description: ['', Validators.required],
      price: ['', Validators.required],
      type: ['', Validators.required],
      material: ['', Validators.required],
      weight: '',
      width: '',
      height: '',
      length: ''
    });

    this.productMaterials = this.productService.getProductMaterials();
    this.productTypes = this.productService.getProductTypes();

    const id = this.activatedRoute.snapshot.params['id'];
    if (id) {
      this.productService.getById(+id)
        .subscribe(product => {
          this.form.controls['id'].setValue(product.Id);
          this.form.controls['title'].setValue(product.Title);
          this.form.controls['description'].setValue(product.Description);
          this.form.controls['price'].setValue(product.Price);
          this.form.controls['type'].setValue(product.ProductTypeId);
          this.form.controls['weight'].setValue(product.Weight);
          this.form.controls['width'].setValue(product.Width);
          this.form.controls['height'].setValue(product.Height);
          this.form.controls['length'].setValue(product.Length)
          this.form.controls['material'].setValue(product.ProductMaterialId);
          this.title = `Edit '${product.Title}' product`
        });
    }
  }

  submitForm(form: FormGroup) {
    const product: Product = {
      Id: this.form.value.id || 0,
      Title: this.form.value.title,
      Description: this.form.value.description,
      Price: this.form.value.price,
      ProductTypeId: this.form.value.type,
      Weight: this.form.value.weight,
      Width: this.form.value.width,
      Height: this.form.value.height,
      Length: this.form.value.length,
      ProductMaterialId: this.form.value.material,
      CreateDate: new Date()
    };
    this.productService.saveProduct(product).subscribe(data => {
      if (data.success) {
        product.Id ? this.router.navigate(['/product/view', product.Id]) : this.router.navigate(['/product/list/1']);
      }
    });
  }
}
