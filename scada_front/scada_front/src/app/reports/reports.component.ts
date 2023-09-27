import {Component, OnInit} from '@angular/core';

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

  














  timePeriodReportSeleceted(){
    this.timePeriodReport = true;
    this.priorityReport = false;
    this.tagRecordsTimeReport = false;
    this.aiReports = false;
    this.diReports = false;
    this.specTagReport = false;
  }

  priorityReportSeleceted(){
    this.timePeriodReport = false;
    this.priorityReport = true;
    this.tagRecordsTimeReport = false;
    this.aiReports = false;
    this.diReports = false;
    this.specTagReport = false;
  }

  tagRecordsTimeReportSelected(){
    this.timePeriodReport = false;
    this.priorityReport = false;
    this.tagRecordsTimeReport = true;
    this.aiReports = false;
    this.diReports = false;
    this.specTagReport = false;
  }

  aiReportsSelected(){
    this.timePeriodReport = false;
    this.priorityReport = false;
    this.tagRecordsTimeReport = false;
    this.aiReports = true;
    this.diReports = false;
    this.specTagReport = false;
  }

  diReportsSelected(){
    this.timePeriodReport = false;
    this.priorityReport = false;
    this.tagRecordsTimeReport = false;
    this.aiReports = false;
    this.diReports = true;
    this.specTagReport = false;
  }

  specTagReportSelected(){
    this.timePeriodReport = false;
    this.priorityReport = false;
    this.tagRecordsTimeReport = false;
    this.aiReports = false;
    this.diReports = false;
    this.specTagReport = true;
  }
  ngOnInit() {
  }
}
