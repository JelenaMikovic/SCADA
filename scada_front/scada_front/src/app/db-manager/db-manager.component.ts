import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { TagService, TagDTO } from "../services/tag.service";
import { MatDialog } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";

@Component({
  selector: 'app-db-manager',
  templateUrl: './db-manager.component.html',
  styleUrls: ['./db-manager.component.css']
})
export class DbManagerComponent implements OnInit {
  isInputTagsClicked: boolean = false;
  isOutputTagsClicked: boolean = false;
  displayedColumnsOutput = ['name', 'type', 'description', 'value', 'actions'];
  displayedColumnsInput = ['name', 'type', 'description', 'scanTime', 'scan', 'actions'];
  allTags: TagDTO[] = [];
  editTag!: TagDTO;
  openEdit: boolean = false
  openCreateAI: boolean = false
  openCreateDI: boolean = false

  constructor(private dialog: MatDialog, private tagService: TagService, private snackBar: MatSnackBar, private router: Router) { }

  getAllTags() {
    this.tagService.getTags().subscribe({
      next: (result) => {
        this.allTags = result as TagDTO[];
      },
      error: (error) => {
        // Handle errors here, you can display an error message if needed
        console.error('Error fetching tags:', error);
      },
    });
  }

  ngOnInit(): void {
    this.getAllTags();
  }

  edit(tag: TagDTO) {
    this.editTag = tag
    this.openEdit = true
    console.log(this.openEdit)
  }

  delete(id: number) {
    this.tagService.deleteTag(id).subscribe({
      next: (result) => {
        alert("Tag deleted")
      },
      error: (error) => {
        // Handle errors here, you can display an error message if needed
        console.error('Error fetching tags:', error);
      },
    });
  }

  sendEdit() {
    const dto: TagDTO = this.editTag

    if ((document.getElementById('name') as HTMLInputElement).value.trim() != "") {
      dto.name = (document.getElementById('name') as HTMLInputElement).value.trim()
    }
    if ((document.getElementById('ioAddress') as HTMLInputElement).value.trim() != "") {
      dto.ioAddress = (document.getElementById('ioAddress') as HTMLInputElement).value.trim()
    }
    if ((document.getElementById('description') as HTMLInputElement).value.trim() != "") {
      dto.description = (document.getElementById('description') as HTMLInputElement).value.trim()
    }
    if ((document.getElementById('value') as HTMLInputElement).value.trim() != "") {
      dto.value = parseFloat((document.getElementById('value') as HTMLInputElement).value.trim())
    }
    if ((document.getElementById('lowLimit') as HTMLInputElement).value.trim() != "") {
      dto.lowLimit = parseFloat((document.getElementById('lowLimit') as HTMLInputElement).value.trim())
    }
    if ((document.getElementById('highLimit') as HTMLInputElement).value.trim() != "" && this.editTag.highLimit != null) {
      dto.highLimit = parseFloat((document.getElementById('highLimit') as HTMLInputElement).value.trim())
    }
    if ((document.getElementById('scanTime') as HTMLInputElement).value.trim() != "" && this.editTag.scanTime != null) {
      dto.scanTime = parseFloat((document.getElementById('scanTime') as HTMLInputElement).value.trim())
    }
    if ((document.getElementById('unit') as HTMLInputElement).value.trim() != "" && this.editTag.scanTime != null) {
      dto.unit = (document.getElementById('unit') as HTMLInputElement).value.trim()
    }

    this.tagService.editTag(dto).subscribe({
      next: (result) => {
        alert("Tag edited")
        this.openEdit = false
      },
      error: (error) => {
        // Handle errors here, you can display an error message if needed
        console.error('Error fetching tags:', error);
      },
    });
  }

  close(){
    this.openEdit = false
    this.openCreateAI = false
    this.openCreateDI = false
  }

  createAI(){
    this.openCreateAI = true
  }

  sendCreateAI() {
    const dto = {
      name : (document.getElementById('nameC') as HTMLInputElement).value.trim(),
      ioAddress : (document.getElementById('ioAddressC') as HTMLInputElement).value.trim(),
      description : (document.getElementById('descriptionC') as HTMLInputElement).value.trim(),
      value : parseFloat((document.getElementById('valueC') as HTMLInputElement).value.trim()),
      lowLimit : parseFloat((document.getElementById('lowLimitC') as HTMLInputElement).value.trim()),
      highLimit : parseFloat((document.getElementById('highLimitC') as HTMLInputElement).value.trim()),
      scanTime : parseFloat((document.getElementById('scanTimeC') as HTMLInputElement).value.trim()),
      isScanOn: false,
      type: "AI",
      unit: (document.getElementById('unitC') as HTMLInputElement).value.trim()
    }

    this.tagService.createTag(dto).subscribe({
      next: (result) => {
        alert("Tag edited")
        this.openCreateAI = false
      },
      error: (error) => {
        // Handle errors here, you can display an error message if needed
        console.error('Error fetching tags:', error);
      },
    });
  }

  createDI(){
    this.openCreateDI = true
  }

  sendCreateDI() {
    const dto = {
      name : (document.getElementById('nameD') as HTMLInputElement).value.trim(),
      ioAddress : (document.getElementById('ioAddressD') as HTMLInputElement).value.trim(),
      description : (document.getElementById('descriptionD') as HTMLInputElement).value.trim(),
      value : parseFloat((document.getElementById('valueD') as HTMLInputElement).value.trim()),
      scanTime : parseFloat((document.getElementById('scanTimeD') as HTMLInputElement).value.trim()),
      isScanOn: false,
      type: "DI",
    }

    this.tagService.createTag(dto).subscribe({
      next: (result) => {
        alert("Tag edited")
        this.openCreateDI = false
      },
      error: (error) => {
        // Handle errors here, you can display an error message if needed
        console.error('Error fetching tags:', error);
      },
    });
  }
}
