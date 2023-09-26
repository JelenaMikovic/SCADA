import {Component, OnInit} from '@angular/core';
import {AlarmDTO, AlarmRecordDTO, AlarmService} from "../services/alarm.service";
import {error} from "@angular/compiler-cli/src/transformers/util";

@Component({
  selector: 'app-alarm-display',
  templateUrl: './alarm-display.component.html',
  styleUrls: ['./alarm-display.component.css']
})
export class AlarmDisplayComponent implements OnInit{
  allAlarms:AlarmRecordDTO[] = [];

  constructor(private alarmservice:AlarmService) {
  }
  ngOnInit(): void {
    this.getAlarmHistory();
  }

  getAlarmHistory(){
    this.alarmservice.getAlarmRecords().subscribe({
      next:(result)=>{
        console.log("RES ALARMI");
        console.log(result);
        this.allAlarms = result as AlarmRecordDTO[];
      },
      error:(error)=>{
        console.error("Error fetching alarm history:",error());},
    })
  }

}
