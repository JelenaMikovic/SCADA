import {Component, OnInit} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {TagDTO, TagService} from "../services/tag.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Router} from "@angular/router";

@Component({
  selector: 'app-trending',
  templateUrl: './trending.component.html',
  styleUrls: ['./trending.component.css']
})
export class TrendingComponent implements OnInit{
  allTags: TagDTO[] = [];
  constructor(private dialog: MatDialog, private tagService: TagService, private snackBar: MatSnackBar, private router: Router) { }

  getAllTags() {
    this.tagService.getTags().subscribe({
      next: (result) => {
        console.log(result);
        for (let tag of result){
          if (tag.tagType=="AI" || tag.tagType=="DI"){
            if (tag.isScanOn){
              this.allTags.push(tag);
            }
          }
        }
        console.log("PROTRCAO")
        console.log(this.allTags);
        // this.allTags = result as TagDTO[];
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
