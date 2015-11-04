(add-to-list 'load-path' "~/.emacs.d/_emacs/")
(add-to-list 'custom-theme-load-path "~/.emacs.d/themes/")


;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;彩虹猫动画;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;彩虹猫动画
(load-file "~/.emacs.d/nyan-mode/nyan-mode.el")

;;彩虹猫做动画
(setq default-mode-line-format  
      (list ""  
            'mode-line-modified  
            "<"  
            "kirchhoff"  
            "> "  
            "%10b"  
            '(:eval (when nyan-mode (list (nyan-create))));;注意添加此句到你的format配置列表中  
            " "  
            'default-directory  
            " "  
            "%[("  
            'mode-name  
            'minor-mode-list  
            "%n"  
            'mode-line-process  
            ")%]--"  
            "Line %l--"  
            '(-1 . "%P")  
            "-%-"))  
(nyan-mode t);;启动nyan-mode  
(nyan-start-animation);;开始动画
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;; 关闭工具栏
;;(tool-bar-mode 0)

;;防止页面滚动时跳动，
;;可以在靠近屏幕边沿3行时就开始滚动
scroll-margin 3
;;scroll-step 1 设置为每次翻滚一行，可以使页面更连续
(setq scroll-step 1 scroll-margin 3 scroll-conservatively 10000)

;;指定目下全局搜索
(global-set-key (kbd "C-f") 'rgrep)

;;文件搜索
(global-set-key [f3] 'find-name-dired)

;;搜索文件中相同的字符串
(global-set-key [f4] 'list-matching-lines)

;;shell 开启
(global-set-key [f5] 'shell) 

;;eshell 开启
(global-set-key [f6] 'eshell)

;设置TAB默认的宽度
;; (setq default-tab-width 4)

;;选中注释
(defun qiang-comment-dwim-line (&optional arg)
  "Replacement for the comment-dwim command. If no region is selected and current line is not blank and we are not at the end of the line, then comment current line. Replaces default behaviour of comment-dwim, when it inserts comment at the end of the line."
  (interactive "*P")
  (comment-normalize-vars)
  (if (and (not (region-active-p)) (not (looking-at "[ \t]*$")))
      (comment-or-uncomment-region (line-beginning-position) (line-end-position))
    (comment-dwim arg)))
(global-set-key (kbd "C-/")'qiang-comment-dwim-line)

;; 显示行号
(global-linum-mode 1)

(load-theme 'molokai t)

;; (set-default-font "Comic Sans MS Bold 14")
;;set transparent effect
 (global-set-key [(f8)] 'loop-alpha)
   (setq alpha-list '((100 100) (95 65) (85 55) (75 45) (65 35)))
   (defun loop-alpha ()
      (interactive)
      (let ((h (car alpha-list)))                ;; head value will set to
      ((lambda (a ab)
          (set-frame-parameter (selected-frame) 'alpha (list a ab))
          (add-to-list 'default-frame-alist (cons 'alpha (list a ab)))
         ) (car h) (car (cdr h)))
      (setq alpha-list (cdr (append alpha-list (list h))))
     )
)
