"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var testing_2 = require("@angular/common/http/testing");
var home_component_1 = require("./home.component");
var ngx_pagination_1 = require("ngx-pagination");
var ng2_search_filter_1 = require("ng2-search-filter");
var stories_service_1 = require("../shared/stories.service");
describe('HomeComponent', function () {
    var component;
    var fixture;
    beforeEach(testing_1.async(function () {
        testing_1.TestBed.configureTestingModule({
            imports: [ngx_pagination_1.NgxPaginationModule, ng2_search_filter_1.Ng2SearchPipeModule, testing_2.HttpClientTestingModule],
            providers: [stories_service_1.StoriesService],
            declarations: [home_component_1.HomeComponent]
        })
            .compileComponents();
    }));
    beforeEach(function () {
        fixture = testing_1.TestBed.createComponent(home_component_1.HomeComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it("should have a page title as 'Hacker News Stories'", testing_1.async(function () {
        fixture = testing_1.TestBed.createComponent(home_component_1.HomeComponent);
        component = fixture.debugElement.componentInstance;
        expect(component.title).toEqual('Hacker News Stories');
    }));
});
//# sourceMappingURL=home.component.spec.js.map