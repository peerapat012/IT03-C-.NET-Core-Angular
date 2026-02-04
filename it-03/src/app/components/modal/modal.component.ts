import { NgIf } from '@angular/common';
import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { FormsModule } from '@angular/forms';
import { MutipleApproveOrRejectRequest } from '../../models/request.model';

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

  reasonText = "";


  async onSubmit() {
    const approveOrRejectRequest: MutipleApproveOrRejectRequest = {
      ids: this.selectedIds,
      responseReason: this.reasonText
    }
    if (this.isApprove == true) {
      await this.apiService.MutipleApprove(approveOrRejectRequest)
      this.reasonText = "";
      this.onSuccess.emit();
    } else {
      await this.apiService.MutipleReject(approveOrRejectRequest)
      this.reasonText = "";
      this.onSuccess.emit();
    }
  }

} 
