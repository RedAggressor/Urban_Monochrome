import { About } from '../../widgets/About/About';
import { HomePageBanner } from '../../widgets/HomePageBanner/HomePageBanner';
import { New } from '../../widgets/New/New';
import cl from './homePage.module.scss';

export const HomePage = () => {
  return (
    <div className={cl['home-page-main']}>
      <HomePageBanner />
      <About />
      <New />
    </div>
  );
};
