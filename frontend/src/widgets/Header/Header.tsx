import '../../widgets/Header/header.scss';

import { HeaderMobile } from '../../shared/ui-kit/HeaderMobile/HeaderMobile';
import { HeaderDesktop } from '../../shared/ui-kit/HeaderDesktop/HeaderDesktop';

export const Header = () => {
  return (
    <header className="header">
      <HeaderMobile />
      <HeaderDesktop />
    </header>
  )
}
