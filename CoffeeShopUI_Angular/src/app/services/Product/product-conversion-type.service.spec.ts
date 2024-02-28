import { TestBed } from '@angular/core/testing';

import { ProductConversionTypeService } from './product-conversion-type.service';

describe('ProductConversionTypeService', () => {
  let service: ProductConversionTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductConversionTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
