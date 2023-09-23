import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {TagService,TagDTO} from "../services/tag.service";
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
  displayedColumnsOutput = ['name', 'type', 'description', 'value', 'actions'];
  displayedColumnsInput = ['name', 'type', 'description', 'scanTime', 'scan', 'actions'];
  allTags : TagDTO[] = [];


  constructor(private dialog: MatDialog, private tagService: TagService, private snackBar: MatSnackBar, private router: Router){}

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
}
