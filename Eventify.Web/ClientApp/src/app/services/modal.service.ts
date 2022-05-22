import { Injectable } from '@angular/core';
import { BsModalService, BsModalRef, ModalOptions } from 'ngx-bootstrap/modal';

@Injectable()
export class ModalService {

  constructor(private readonly bsModalService: BsModalService) { }

  private modalRef: BsModalRef;

  openModal(component: any, initialState: object, className?: string) {
    const modalOptions = new ModalOptions();
    modalOptions.class = `modal-md ${className}`;
    modalOptions.backdrop = 'static';
    modalOptions.keyboard = false;
    modalOptions.initialState = initialState;

    this.modalRef = this.bsModalService.show(component, modalOptions);
    return this.modalRef;
  }
}