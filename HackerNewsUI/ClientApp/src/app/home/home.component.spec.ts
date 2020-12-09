import { ComponentFixture, TestBed, async, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HomeComponent } from './home.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { StoriesService } from '../shared/stories.service';

describe('HomeComponent', () => {
let component: HomeComponent;
let fixture: ComponentFixture<HomeComponent>;

beforeEach(async(() => {
  TestBed.configureTestingModule({
    imports: [NgxPaginationModule, Ng2SearchPipeModule, HttpClientTestingModule],
    providers: [StoriesService],
    declarations: [HomeComponent]
  })
    .compileComponents();
}));

beforeEach(() => {
  fixture = TestBed.createComponent(HomeComponent);
  component = fixture.componentInstance;
  fixture.detectChanges();
});

  it(`should have a page title as 'Hacker News Stories'`, async(() => {
  fixture = TestBed.createComponent(HomeComponent);
  component = fixture.debugElement.componentInstance;
    expect(component.title).toEqual('Hacker News Stories');
}));
});
