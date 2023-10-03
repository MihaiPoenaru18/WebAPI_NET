import { Component } from '@angular/core';
import { AboutUsInterface } from './about-us.interfaces';

@Component({
  selector: 'about-us',
  templateUrl: './about-us.component.html',
  styleUrls: ['./about-us.component.css']
})
export class AboutUsComponent {
   contents:AboutUsInterface[] = [
    { title:"About Us", text:"Welcome to Coffee Master from Peru, where every cup is a journey through the lush landscapes and rich coffee traditions of Peru. We are passionate about delivering an exceptional coffee experience that not only tantalizes your taste buds but also connects you with the heart and soul of Peruvian coffee culture."},
    { title:"Our Story", text:"At Coffee Master from Peru, our story is intertwined with the history of coffee in the high-altitude regions of Peru. Our journey began with a simple love for coffee and a desire to showcase the incredible flavors and stories behind each cup. We embarked on a mission to source the finest Peruvian coffee beans, cultivated by dedicated farmers who have mastered the art of coffee cultivation over generations."},
    { title:"The Peruvian Coffee Experience", text:"Peru is renowned for its diverse coffee regions, each with its own unique terroir, producing beans with distinctive flavors and profiles. We take pride in working closely with local farmers and cooperatives to bring you a curated selection of single-origin coffees that capture the essence of Peru's coffee heritage."},
    { title:"Our Commitment to Quality", text:"Quality is at the core of everything we do. From the moment coffee cherries are hand-picked at the peak of ripeness to the careful roasting process, we ensure that every step is executed with precision and passion. Our commitment to quality extends to our artisanal brewing methods, delivering you a cup of coffee that is nothing short of extraordinary."},
    { title:" Community and Sustainability", text:"We believe in giving back to the communities that have made our journey possible. Coffee Master from Peru is dedicated to supporting sustainable farming practices and empowering coffee-producing communities. When you enjoy a cup of our coffee, you are not just savoring the taste but also contributing to the well-being of the Peruvian coffee growers and their families."},
    { title:" Join Us", text:"We invite you to join us on a journey through the flavors, stories, and traditions of Peru's coffee world. Whether you're a seasoned coffee connoisseur or new to the world of specialty coffee, we are here to share our passion with you. Visit our café, explore our unique coffee blends, and let us be your guides to the coffee treasures of Peru."}
  ];
  
   currentIndex: number = 0;
   timeoutId?: number;
   ngOnInit(): void {
    this.resetTimer();
  }
  ngOnDestroy() {
    window.clearTimeout(this.timeoutId);
  }
  resetTimer() {
    if (this.timeoutId) {
      window.clearTimeout(this.timeoutId);
    }
    this.timeoutId = window.setTimeout(() => this.goToNext(), 9000);
  }
 
  goToNext(): void {
    const isLastSlide = this.currentIndex === this.contents.length - 1;
    const newIndex = isLastSlide ? 0 : this.currentIndex + 1;
    this.resetTimer();
    this.currentIndex = newIndex;
  }
  goToSlide(contentIndex: number): void {
    this.resetTimer();
    this.currentIndex = contentIndex;
  }
  getCurrentText() {
    return this.contents[this.currentIndex].text;
  }
  getCurrentTitle() {
    return this.contents[this.currentIndex].title;
  }
}
