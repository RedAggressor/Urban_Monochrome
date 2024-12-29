import '../HeaderDesktop/headerDesktop.scss';

import { NavLinks, NavLinksOrigin } from '../NavLinks/NavLinks';
import { SearchIcon } from '../../icons/icons_header/SearchIcon';
import { Likeicon } from '../../icons/icons_header/Likeicon';
import { AccountIcon } from '../../icons/icons_header/AccountIcon';
import { CartIcon } from '../../icons/icons_header/CartIcon';

export const HeaderDesktop = () => {
  return (
    <div className="header__desktop">
      <a href="/" className="icon header_logo">
        <img src="/logo.png" alt="Company Logo" />
      </a>

      <NavLinks className="header__nav" origin={NavLinksOrigin.Header} />

      <div className="header__icons">
        <a href="#" className="icon search__icon">
          <SearchIcon />
        </a>
        <a href="#" className="icon">
          <Likeicon />
        </a>
        <a href="#" className="icon">
          <AccountIcon />
        </a>
        <a href="#" className="icon">
          <CartIcon />
        </a>
      </div>
    </div>
  );
};
