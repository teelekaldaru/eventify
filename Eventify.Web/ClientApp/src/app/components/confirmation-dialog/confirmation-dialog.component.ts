import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subject } from 'rxjs';

@Component({
  selector: 'confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html',
  styleUrls: ['./confirmation-dialog.component.scss']
})
export class ConfirmationDialogComponent implements OnInit {

  content: string;
  okLabel: string = "OK";
  cancelLabel: string = "Tühista";

  onConfirm: () => void;

  onClose: Subject<boolean>;

  constructor(
    private readonly bsModalRef: BsModalRef
  ) { }

  ngOnInit(): void {
    this.onClose = new Subject();
  }

  confirm(): void {
    this.onConfirm.call(this);
    this.bsModalRef.hide();
  }

  cancel(): void {
    this.onClose.next(false);
    this.bsModalRef.hide();
  }
}
