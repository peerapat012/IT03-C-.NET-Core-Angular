import { Component, EventEmitter, Output, output } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ModalComponent } from "./modal/modal.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ModalComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'it-03';

  isModalOpen = false;
  isApprove = false;

  selectedIds: number[] = [];

  @Output() open = new EventEmitter<void>();

  toggleSelection(id: number, checked: boolean) {
    if (checked)
      this.selectedIds.push(id);
    else {
      this.selectedIds = this.selectedIds.filter(x => x !== id);
    }
  }

  openModal(isApprove = false) {
    this.isApprove = isApprove;
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
    this.isApprove = false;
  }
}
