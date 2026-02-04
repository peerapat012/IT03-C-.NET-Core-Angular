import { Component, EventEmitter, Output, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ModalComponent } from "./components/modal/modal.component";
import { ApiService } from './services/api.service';
import { Request } from './models/request.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ModalComponent, CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'it-03';

  datas: Request[] = [];
  isApprove = false;
  modal = null;

  @Output() open = new EventEmitter<void>();

  selectedIds: number[] = [];

  constructor(private apiService: ApiService) { }

  async ngOnInit() {
    await this.loadData();
  }

  async loadData() {
    try {
      this.datas = await this.apiService.getDatas();
      this.selectedIds = [];
      console.log('Data received:', this.datas);
    } catch (error) {
      console.error('Failed to load data:', error);
    }
  }

  async onRefresh() {
    await this.loadData();
  }


  toggleSelection(id: number, checked: boolean) {
    if (checked)
      this.selectedIds.push(id);
    else {
      this.selectedIds = this.selectedIds.filter(x => x !== id);
    }
  }

  onCheckboxChange(event: any, id: number) {
    if (event.target.checked) {
      // Add ID to array if checked
      this.selectedIds.push(id);
    } else {
      // Remove ID from array if unchecked
      const index = this.selectedIds.indexOf(id);
      if (index > -1) {
        this.selectedIds.splice(index, 1);
      }
    }
    console.log('Selected IDs:', this.selectedIds);
  }

  isChecked(id: number): boolean {
    return this.selectedIds.includes(id);
  }

  isDisable(status: number): boolean {
    return (status == 0 || status == 1)
  }

  openModal(isApprove: boolean) {
    this.isApprove = isApprove;
  }

}
