import { NgIf } from '@angular/common';
import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { FormsModule } from '@angular/forms';
import { ApproveOrRejectRequest } from '../../models/request.model';

@Component({
  selector: 'app-modal',
  imports: [FormsModule],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.css'
})
export class ModalComponent {

  @Input() selectedIds: number[] = [];
  @Input() isApprove: boolean = false;
  @Output() close = new EventEmitter<void>();
  @Output() onSuccess = new EventEmitter<void>();

  constructor(private apiService: ApiService) { }

  approveOrRejectRequest: ApproveOrRejectRequest = {
    responseReason: ""
  }

  async onSubmit() {
    if (this.isApprove == true) {
      await this.apiService.Approve(this.selectedIds[0], this.approveOrRejectRequest)
      this.approveOrRejectRequest.responseReason = "";
      this.onSuccess.emit();
    } else {
      await this.apiService.Reject(this.selectedIds[0], this.approveOrRejectRequest)
      this.approveOrRejectRequest.responseReason = "";
      this.onSuccess.emit();
    }
  }

} 
