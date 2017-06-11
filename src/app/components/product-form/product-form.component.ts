import { Modal } from 'ng2-modal';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductMaterial } from './../../models/productMaterial';
import { ProductType } from './../../models/productType';
import { Observable } from 'rxjs/Observable';
import { ProductService } from './../../services/product.service';
import { Product } from './../../models/product';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, NgForm } from "@angular/forms";

import 'rxjs';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {
  @ViewChild('materialModal') materialModal: Modal;
  @ViewChild('typeModal') typeModal: Modal;

  title: String = 'Create new product';
  productMaterials: Observable<ProductMaterial>;
  productTypes: Observable<ProductType>;
  newProductType: ProductType;
  newProductMaterial: ProductMaterial;
  form: FormGroup;
  product: any = {};
  fileName: String;

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
      image: '',
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
          //this.form.controls['image'].setValue(product.Image);
          this.title = `Edit '${product.Title}' product`
        });
    }
  }

  submitForm(form: FormGroup) {
    console.log(this.form);
    const product: Product = {
      Id: this.form.value.id || 0,
      Title: this.form.value.title,
      Description: this.form.value.description,
      Price: this.form.value.price,
      ProductTypeId: this.form.value.type,
      Image: this.fileName || "default.jpg",
      Weight: this.form.value.weight,
      Width: this.form.value.width,
      Height: this.form.value.height,
      Length: this.form.value.length,
      ProductMaterialId: this.form.value.material,
      CreateDate: new Date(),
    };
    this.productService.saveProduct(product).subscribe(data => {
      if (data.success) {
        product.Id ? this.router.navigate(['/product/view', product.Id]) : this.router.navigate(['/product/list/1']);
      }
    });
  }

  fileEvent(fileInput: any){
    let file = fileInput.target.files[0];
    this.fileName = file.name;
}

  addNewMaterial(form: NgForm) {
    const productMaterial: ProductMaterial = {
      Id: 0,
      Title: form.value.title
    };
    this.productService.addMaterial(productMaterial).subscribe(data => {
      if (data.success == true) {
        this.productMaterials = this.productService.getProductMaterials();
      }
      this.materialModal.close();
    })
  }

  addNewType(form: NgForm) {
    const productType: ProductType = {
      Id: 0,
      Title: form.value.title
    };
    this.productService.addType(productType).subscribe(data => {
      if (data.success == true) {
        this.productTypes = this.productService.getProductTypes();
      }
      this.typeModal.close();
    })
  }
}
