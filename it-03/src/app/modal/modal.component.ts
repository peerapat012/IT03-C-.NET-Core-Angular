import { NgIf } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-modal',
  imports: [NgIf],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.css'
})
export class ModalComponent {
  @Input() isOpen = false;
  @Input() isApprove = false;
  @Output() close = new EventEmitter<void>();
} 
