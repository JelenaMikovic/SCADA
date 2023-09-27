import {Component, OnInit} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {TagDTO, TagService} from "../services/tag.service";
import {TagRecordDTO} from "../services/web-socket.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Router} from "@angular/router";
import {HttpTransportType, HubConnectionBuilder, LogLevel} from "@microsoft/signalr";


@Component({
  selector: 'app-trending',
  templateUrl: './trending.component.html',
  styleUrls: ['./trending.component.css']
})
export class TrendingComponent implements OnInit{
  allTags: TagDTO[] = [];
  tagUpdateConnection: any;
  alarmUpdateConnection: any;

  constructor(private dialog: MatDialog, private tagService: TagService, private snackBar: MatSnackBar, private router: Router) { }

  getAllTags() {
    this.tagService.getTags().subscribe({
      next: (result) => {
        //console.log(result);
        for (let tag of result){
          if (tag.tagType=="AI" || tag.tagType=="DI"){
            if (tag.isScanOn){
              this.allTags.push(tag);
            }
          }
        }
        console.log(this.allTags);
      },
      error: (error) => {
        // Handle errors here, you can display an error message if needed
        console.error('Error fetching tags:', error);
      },
    });
  }

  updateTag(update:any){
    //console.log(update)
    for (let tag of this.allTags){
      if (tag.id == update.tagId){
        tag.value = update.value;
      }
    }
  }

  initTagUpdateWebSocket() {
    this.tagUpdateConnection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Debug)
      .withUrl('https://localhost:7012/hub/updateTag', {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      })
      .build();
    this.tagUpdateConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(() => console.log('Error while starting connection: '))
    this.tagUpdateConnection.on('tag', (from: string, body: string) => {
      //console.log(from, body);
      this.updateTag(from);
    });
  }

  initAlarmUpdateWebSocket() {
    this.alarmUpdateConnection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Debug)
      .withUrl('https://localhost:7012/hub/updateAlarm', {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      })
      .build();
    this.alarmUpdateConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(() => console.log('Error while starting connection: '))
    this.alarmUpdateConnection.on('alarm', (from: string, body: string) => {
      console.log(from, body);
      this.handleAlarmUpdateWebSocket(from);
    });
  }

  handleAlarmUpdateWebSocket(alarmRecord: any){
    //console.log(alarmRecord);
    this.snackBar.open('Alarm for tag with id:' + alarmRecord.tagId + ". Priority: " + alarmRecord.priority +". Value when tag alarm occured: " + alarmRecord.value + " .",'Close',{duration:3000});
  }

  ngOnInit(): void {
    this.getAllTags();
    this.initTagUpdateWebSocket();
    this.initAlarmUpdateWebSocket();
  }


}
