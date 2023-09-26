import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { TagService, TagDTO } from "../services/tag.service";
import { MatDialog } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { AlarmDTO, AlarmService } from '../services/alarm.service';
import { DeviceDTO, DeviceService } from '../services/device.service';

@Component({
  selector: 'app-db-manager',
  templateUrl: './db-manager.component.html',
  styleUrls: ['./db-manager.component.css']
})
export class DbManagerComponent implements OnInit {
  allTags: TagDTO[] = [];
  allDevices: DeviceDTO[] = [];
  allOutputDevices: DeviceDTO[] = [];
  editTag!: TagDTO;
  openEdit: boolean = false
  openCreateAI: boolean = false
  openCreateDI: boolean = false
  openCreateAO: boolean = false
  openCreateDO: boolean = false
  allAlarms: AlarmDTO[] = []
  openAlarms: boolean = false
  openAddAlarm: boolean = false
  selectedType!: number;
  selectedPriority!: number;
  selectedId!: number;
  selectedAIAddress!: string;
  selectedDIAddress!: string;
  selectedAOAddress!: string;
  selectedDOAddress!: string;

  constructor(private dialog: MatDialog, private tagService: TagService, private deviceService: DeviceService, private alarmService: AlarmService, private snackBar: MatSnackBar, private router: Router) { }

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

  getAllDevices() {
    this.deviceService.getDevices().subscribe({
      next: (result) => {
        this.allDevices = result as DeviceDTO[];
      },
      error: (error) => {
        console.error('Error fetching tags:', error);
      },
    });
    this.deviceService.getOutputDevices().subscribe({
      next: (result) => {
        this.allOutputDevices = result as DeviceDTO[];
      },
      error: (error) => {
        console.error('Error fetching tags:', error);
      },
    });
  }

  getAllAlarms(tagId: number) {
    this.alarmService.getTagsAlarms(tagId).subscribe({
      next: (result) => {
        this.allAlarms = result as AlarmDTO[];
        this.openAlarms = true;
        this.selectedId = tagId
        console.log(result)
      },
      error: (error) => {
        // Handle errors here, you can display an error message if needed
        console.error('Error fetching tags:', error);
      },
    });
  }

  ngOnInit(): void {
    this.getAllTags();
    this.getAllDevices();
  }

  toggle(tag: TagDTO) {
    this.tagService.toggleTag(tag.id).subscribe({
      next: (result) => {
        tag.isScanOn = !tag.isScanOn;
      },
      error: (error) => {
        // Handle errors here, you can display an error message if needed
        console.error('Error fetching tags:', error);
      },
    });
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
    this.openCreateAO = false
    this.openCreateDO = false
    this.openAlarms = false
    this.openAddAlarm = false
  }

  createAI(){
    this.openCreateAI = true
  }

  sendCreateAI() {
    const dto = {
      name : (document.getElementById('nameC') as HTMLInputElement).value.trim(),
      ioAddress : this.selectedAIAddress.trim(),
      description : (document.getElementById('descriptionC') as HTMLInputElement).value.trim(),
      value : 0,
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
      ioAddress : this.selectedDIAddress.trim(),
      description : (document.getElementById('descriptionD') as HTMLInputElement).value.trim(),
      value : 0,
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

  deleteAlarm(id: number){
    this.alarmService.deleteAlarm(id).subscribe({
      next: (result) => {
        alert("Alarm deleted")
        this.openAlarms = false
      },
      error: (error) => {
        // Handle errors here, you can display an error message if needed
        console.error('Error fetching tags:', error);
      },
    });
  }

  addAlarm(){
    this.openAddAlarm = true
  }

  sendAlarm(){
    var dto = {
      value: parseFloat((document.getElementById('valueA') as HTMLInputElement).value.trim()),
      priority: this.selectedPriority,
      type: this.selectedType,
      tagId: this.selectedId
    }
    console.log(this.selectedId)
    this.alarmService.addAlarm(dto).subscribe({
      next: (result) => {
        alert("Alarm added")
        this.openAddAlarm = false
      },
      error: (error) => {
        // Handle errors here, you can display an error message if needed
        console.error('Error fetching tags:', error);
      },
    });
  }

  createAO(){
    this.openCreateAO = true
  }

  sendCreateAO() {
    const dto = {
      name : (document.getElementById('nameCO') as HTMLInputElement).value.trim(),
      ioAddress : this.selectedAOAddress.trim(),
      description : (document.getElementById('descriptionCO') as HTMLInputElement).value.trim(),
      value : parseFloat((document.getElementById('valueCO') as HTMLInputElement).value.trim()),
      lowLimit : parseFloat((document.getElementById('lowLimitCO') as HTMLInputElement).value.trim()),
      highLimit : parseFloat((document.getElementById('highLimitCO') as HTMLInputElement).value.trim()),
      type: "AO",
      unit: (document.getElementById('unitCO') as HTMLInputElement).value.trim()
    }

    this.tagService.createTag(dto).subscribe({
      next: (result) => {
        alert("Tag edited")
        this.openCreateAO = false
      },
      error: (error) => {
        // Handle errors here, you can display an error message if needed
        console.error('Error fetching tags:', error);
      },
    });
  }

  createDO(){
    this.openCreateDO = true
  }

  sendCreateDO() {
    const dto = {
      name : (document.getElementById('nameDO') as HTMLInputElement).value.trim(),
      ioAddress : this.selectedDOAddress.trim(),
      description : (document.getElementById('descriptionDO') as HTMLInputElement).value.trim(),
      value : parseFloat((document.getElementById('valueDO') as HTMLInputElement).value.trim()),
      type: "DO",
    }

    this.tagService.createTag(dto).subscribe({
      next: (result) => {
        alert("Tag edited")
        this.openCreateDO = false
      },
      error: (error) => {
        // Handle errors here, you can display an error message if needed
        console.error('Error fetching tags:', error);
      },
    });
  }

}
