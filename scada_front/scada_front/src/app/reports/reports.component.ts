import {Component, OnInit} from '@angular/core';
import {AlarmRecordDTO, AlarmService} from "../services/alarm.service";
import {TagRecordDTO} from "../services/web-socket.service";
import {TagService} from "../services/tag.service";
import {DeviceDTO, DeviceService} from "../services/device.service";

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit{
  timePeriodReport:boolean = false;
  priorityReport:boolean = false;
  tagRecordsTimeReport:boolean = false;
  aiReports:boolean = false;
  diReports:boolean = false;
  specTagReport:boolean = false;
  selectedPriority!:string;

  alarmRecords:AlarmRecordDTO[]=[];
  tagRecords:TagRecordDTO[]=[];
  selectedIOAddress!:string;
  allOutputDevices:DeviceDTO[]=[];

  constructor(private alarmService:AlarmService, private tagService:TagService, private deviceService:DeviceService) {
  }


  getTimePeriodReportData(){
    var from:Date = new Date((document.getElementById('from') as HTMLInputElement).value);
    var to:Date = new Date((document.getElementById('to') as HTMLInputElement).value);
    this.alarmService.getAlarmTimeReport(from,to).subscribe({
      next:(result)=>{
        this.alarmRecords = result as AlarmRecordDTO[];
        console.log(this.alarmRecords)
      }
    })
  }

  getPriorityReportData(){
    this.alarmService.getAlarmPriorityReport(this.selectedPriority).subscribe({
      next:(result)=>{
        this.alarmRecords = result as AlarmRecordDTO[];
      }
    })
  }


  getTagRecordsTimeReportData(){
    var from:Date = new Date((document.getElementById('fromTag') as HTMLInputElement).value);
    var to:Date = new Date((document.getElementById('toTag') as HTMLInputElement).value);
    this.tagService.getTagRecordsTimeInterval(from,to).subscribe({
      next:(result)=>{
        this.tagRecords = result as TagRecordDTO[];
      }
    })
  }

  getAIReportsData(){
    this.tagService.getAllAIRecords().subscribe({
      next:(result)=>{
        this.tagRecords = result as TagRecordDTO[];
      }
    })
  }


  getDIReportsData(){
    this.tagService.getAllDIRecords().subscribe({
      next:(result)=>{
        this.tagRecords = result as TagRecordDTO[];
      }
    })
  }

  getSpecTagReportData(){
    this.tagService.getRecordsIO(this.selectedIOAddress).subscribe({
      next:(result)=>{
        this.tagRecords = result as TagRecordDTO[];
      }
    })
  }


  timePeriodReportSeleceted(){
    this.timePeriodReport = true;
    this.priorityReport = false;
    this.tagRecordsTimeReport = false;
    this.aiReports = false;
    this.diReports = false;
    this.specTagReport = false;
    this.tagRecords = [];
    this.alarmRecords = [];
  }

  priorityReportSeleceted(){
    this.timePeriodReport = false;
    this.priorityReport = true;
    this.tagRecordsTimeReport = false;
    this.aiReports = false;
    this.diReports = false;
    this.specTagReport = false;
    this.tagRecords = [];
    this.alarmRecords = [];
  }

  tagRecordsTimeReportSelected(){
    this.timePeriodReport = false;
    this.priorityReport = false;
    this.tagRecordsTimeReport = true;
    this.aiReports = false;
    this.diReports = false;
    this.specTagReport = false;
    this.tagRecords = [];
    this.alarmRecords = [];
  }

  aiReportsSelected(){
    this.timePeriodReport = false;
    this.priorityReport = false;
    this.tagRecordsTimeReport = false;
    this.aiReports = true;
    this.diReports = false;
    this.specTagReport = false;
    this.tagService.getAllAIRecords().subscribe({
      next:(result)=>{
        this.tagRecords = result as TagRecordDTO[];
      }
    })
  }

  diReportsSelected(){
    this.timePeriodReport = false;
    this.priorityReport = false;
    this.tagRecordsTimeReport = false;
    this.aiReports = false;
    this.diReports = true;
    this.specTagReport = false;
    this.tagService.getAllDIRecords().subscribe({
      next:(result)=>{
        this.tagRecords = result as TagRecordDTO[];
      }
    })
  }

  specTagReportSelected(){
    this.timePeriodReport = false;
    this.priorityReport = false;
    this.tagRecordsTimeReport = false;
    this.aiReports = false;
    this.diReports = false;
    this.specTagReport = true;
    this.deviceService.getAllDevices().subscribe({
      next:(result)=>{
        this.allOutputDevices = result as DeviceDTO[];
        console.log(result)
      }
    })
  }
  ngOnInit() {
  }
}
