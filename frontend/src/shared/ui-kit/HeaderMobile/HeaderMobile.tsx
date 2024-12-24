import '../HeaderMobile/headerMobile.scss';

import { AccountIcon } from "../../icons/icons_header/AccountIcon"
import { CartIcon } from "../../icons/icons_header/CartIcon"
import { Likeicon } from "../../icons/icons_header/Likeicon"
import { MenuIcon } from "../../icons/icons_header/MenuIcon"
import { SearchIcon } from "../../icons/icons_header/SearchIcon"
import { SearchForm } from "../SearchForm/SearchForm"

export const HeaderMobile = () => {
  return (
    <div className="header__mobile">
      <div className="mobile__wrapper">
      <div className="header__left">
        <nav>
          <div className="icon"><MenuIcon /></div>
            <ul className="menu__list">
              <li className="menu__item"><a href="">Shop</a></li>
              <li className="menu__item"><a href="">New</a></li>
              <li className="menu__item"><a href="">Sale</a></li>
          </ul>
          </nav> 
      <a href="#" className="icon"><AccountIcon /></a>
      </div>

      <a href="/" className="icon header_logo">
      <img src="/logo.png" alt="Company Logo" />
      </a>
      <div className="header__right">
        <a href="#" className="icon"><Likeicon /></a>
        <a href="#" className="icon"><CartIcon /></a>
      </div>
      </div>

      <div className="input-container">
      <SearchForm placeholder={"Search here"} icon={<SearchIcon />} />
      </div>
    </div>
  )
}
