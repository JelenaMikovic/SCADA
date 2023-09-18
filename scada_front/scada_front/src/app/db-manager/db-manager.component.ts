import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {TableInputTag, TableOutputTag, TagService} from "../services/tag.service";
import {MatDialog} from "@angular/material/dialog";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-db-manager',
  templateUrl: './db-manager.component.html',
  styleUrls: ['./db-manager.component.css']
})
export class DbManagerComponent implements OnInit{
  isInputTagsClicked: boolean = false;
  isOutputTagsClicked: boolean = false;
  outputTags: TableOutputTag[] = [] ;
  inputTags: TableInputTag[] = [];
  displayedColumnsOutput = ['name', 'type', 'description', 'value', 'actions'];
  displayedColumnsInput = ['name', 'type', 'description', 'scanTime', 'scan', 'actions'];


  constructor(private dialog: MatDialog, private tagService: TagService, private snackBar: MatSnackBar, private router: Router){}

  get dataSource(): TableOutputTag[] | TableInputTag[] {
    return this.isOutputTagsClicked ? this.outputTags : this.inputTags;
  }
  getAllOutputTags(){
    this.tagService.getAllOutputTagsDBManager().subscribe({
      next: (value) => {
        this.outputTags = []
        console.log("succ\n" + JSON.stringify(value));
        this.outputTags = value;
      },
      error: (err) => {
        // this.snackBar.open(err.error, "", {
        //   duration: 3000
        // });
        console.log(err);
      }
    });
  }


  getAllInputTags() {
    this.tagService.getAllInputTags().subscribe({
      next: (value) => {
        this.inputTags = []
        console.log("succ\n" + JSON.stringify(value));
        this.inputTags = value;
        for (let tag of this.inputTags) {
          if (tag.type == 0)
            tag.type = "DIGITAL"
          else
            tag.type = "ANALOG"
        }
      },
      error: (err) => {
        // this.snackBar.open(err.error, "", {
        //   duration: 2700, panelClass: ['snack-bar-server-error']
        // });
        console.log(err);
      }
    });
  }

  onInputTagsClicked(){
    this.isInputTagsClicked = true;
    this.isOutputTagsClicked = false;
    this.getAllInputTags();
  }


  onOutputTagsClicked(){
    this.isOutputTagsClicked = true;
    this.isInputTagsClicked = false;
    this.getAllOutputTags();

  }

  ngOnInit(): void {
  }
}
