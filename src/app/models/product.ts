import { ProductMaterial } from './productMaterial';
import { ProductType } from './productType';
export class Product {
    Id?: Number;
    Title: String;
    Description?: String;
    CreateDate?: Date;
    Price: number;
    ProductTypeId: String;
    ProductMaterialId?: String;
    Weight?: Number;
    Width?: Number;
    Height?: Number;
    Length?: Number;
    Type?: ProductType;
    Material?: ProductMaterial;
    Image?: String;
}